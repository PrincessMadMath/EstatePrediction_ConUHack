var cheerio = require('cheerio');
var _ = require('lodash');
var Promise = require('bluebird');
var request = Promise.promisifyAll(require('request'), { multiArgs: true });

var url = 'http://duproprio.com/house-for-sale-st-nicolas-quebec-en-607377';
return scrape();
var a  = findProperty(url).then(function(a) {
    var b= a;
    var c  = a;
});

function findLatLong(node) {
    var rels = node
        .attr('rel')
        .split('|')
        .map(_.trim);
    return {
        lat: rels[0],
        long: rels[1]
    };
}

function concatIfBetweenNumbers(a, b) {
    if ( !isNaN(a.substr(a.length - 1)) && !isNaN(a.charAt(0)) ) {
        return a.concat(b);
    } else {
        return a.concat(',', b);
    }
}

function removeNumbersComma(str) {
    if (str.indexOf(',') <= -1) {
        return str;
    } else {
        return str
            .split(',')
            .reduce(concatIfBetweenNumbers);
    }
}

function findUrl(node) {
    return {
        url: node
            .find('.colImg a')
            .attr('href')
    };
}

function formatPrices(str) {
    if(str.indexOf('$') > -1) {
        return str
            .split('$')
            .join('')
            .split(',')
            .join('')
            .split('_')
            .join('')
    }
    return str;
}

function fromDashToFloat(str) {
    return parseFloat(str
        .replace('_', '.')
        .slice(0, -1)
    );
}

function formatDimensions(str) {
    var re1 = /((\d+_){1,2})x_((\d+_){1,2})m/;
    var re2 = /((\d+_){1,2})m/;
    var m;
    if ((m = re1.exec(str)) !== null) {
        var result = m.map(fromDashToFloat);
        return result[1] * result[3];
    } else if( (m = re2.exec(str)) !== null) {
        return fromDashToFloat(m[1]);
    }
    return str;
}

function convertNumbers(str) {
    if(!isNaN(str)){
        return parseFloat(str);
    }
    return str;
}


function findAddress($) {
    return {
        address: $('.outsideBox')
            .contents()
            .last()
            .text()
            .trim()
    };
}

function findTown($) {
    return {
        town: $('h1')
            .contents()
            .last()
            .text()
            .trim()
    };
}

function findFinalPrice($) {
    var finalPrice = $('.price-title')
        .contents()
        .first()
        .text()
        .split(':')
        .map(removeNumbersComma)
        .map(formatPrices)
        .map(_.snakeCase)
        .map(convertNumbers);

    return _.fromPairs([finalPrice])

}

function findFeatures($) {
    var features = $('.dynamicList > .left > li, .dynamicList > .right > li').get()
        .map(function (element) {
            return $(element)
                .text().split(':')
                .map(formatPrices)
                .map(removeNumbersComma)
                .map(_.snakeCase)
                .map(formatDimensions)
                .map(convertNumbers);
        });
    return _.fromPairs(features);
}



function scrapMainPage(url) {
    var options = {
        url: url,
        method: 'GET',
        headers: {
            'Accept-Language':'en',
            'Cookie': 'overlaydisplayed=true; dp_shared_session=nribps4c5f3oa5ui5h2gjslia2; _gat=1; _gat_as25n45=1; dp_session2[uuid]=8ce5f0e6b1dbe0ac0e004dc8fe8e5a5cf086803c8dcf84093eebf4cc5315fa04; _sp_id.c781=9f6b1f3498348391.1453563607.1.1453563661.1453563607; _sp_ses.c781=*; _ga=GA1.2.1060607627.1449020072'
        }
    };


    return request.getAsync(options).spread(function(response, html) {
        var $ = cheerio.load(html);

        var arr = $('.searchresult')
            .map(function (index, element) {
                var houseUrl = 'http://duproprio.com/'+findUrl($(element)).url;
                return findProperty(houseUrl)
                    .then(function(result) {
                        return result;
                    });
            }).get();
        return Promise.all(arr);
    });
}

function nextPage(url) {
    var nextPage = parseInt(url[url.length-2])+1;
    return url.substr(0, url.length-2) + nextPage
}

function scrape() {
    var promises =[];
    var url = 'http://duproprio.com/search/?hash=/g-re=8-1-17-114-12-9-5-11-14-15-13-4-16-31-6-10-7-115-3-257-2/s-pmin=0/s-pmax=99999999/s-build=1/s-parent=1/s-filter=sold/s-days=0/m-pack=house-condo/p-con=main/p-ord=date/p-dir=DESC/pa-ge=1/';
    for(var i =0; i<7087; i++) {
        url = nextPage(url);
        promises.push(scrapMainPage(url).then( function(results) {
            console.log(results);
            return results;
        }));
    }
    return Promise.all(promises);
}

function findProperty(url) {
    return request.getAsync(url).spread(function(response, html) {
        var $ = cheerio.load(html);
        var feature = findFeatures($);
        var finalPrice = findFinalPrice($);
        var town = findTown($);
        var address = findAddress($);
        return _.merge(
            feature,
            finalPrice,
            town,
            address
        );
    });
}


