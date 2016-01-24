var cheerio = require('cheerio');
var _ = require('lodash');
var Promise = require('bluebird');
var request = Promise.promisifyAll(require('request'), { multiArgs: true });
var fs = require('fs');
var path = require('path');


var url = 'http://duproprio.com/house-for-sale-st-nicolas-quebec-en-607377';
return scrape();
/*
var a  = findProperty(url).then(function(a) {
    var b= a;
    var c  = a;
});*/


function findImgUrls($) {
    var imgs = $('.photosList > li > a').map(function(element) {
      return this.attribs.href;
    }).get();
    return {img_urls: imgs};
}

function findLatLong(node) {
    var rels = node
        .attr('rel')
        .split('|')
        .map(_.trim);
    return {
        lat: parseFloat(rels[0]),
        long: parseFloat(rels[1])
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

function findDays(delay) {
    var b = delay.trim().split(' ');
    if(b.length === 4){
        return parseInt(b[0])*30 + parseInt(b[2]);
    } else {
        return parseInt(b[0]);
    }
}

function findSoldInfo(node) {
    var a = node
        .find('.infoSold > p')
        .contents()
        .map(function (index, element) {
            if(element.data && _.trim(element.data) === 'Sold in') {
                var a = element.next.children[0].data;
                return {sold_in: findDays(a)};
            } else if (element.data && _.trim(element.data) === 'on') {
                var b = element.next.children[0].data.split('-');
                return {
                    year_sold: parseInt(b[0]),
                    month_sold: parseInt(b[1])
                };
            }
            return undefined;
        }).get()
        .filter(function(value) {
            return value;
        });
    return _.merge(a[0], a[1]);
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
                            return _.merge(
                                result,
                                findSoldInfo($(element)),
                                findLatLong($(element)),
                                {url: houseUrl})
                        });
                }).get();
            return Promise.all(arr).then(function(results) {
                return results;
            });
    }).catch(function(error) {
        console.log(error);
    });

}

function nextPage(url) {
    var nextPage = parseInt(url[url.length-2])+1;
    return url.substr(0, url.length-2) + nextPage
}

function sleepFor( sleepDuration ){
    var now = new Date().getTime();
    while(new Date().getTime() < now + sleepDuration){ /* do nothing */ }
}

function scrape() {
    var promises =[];
    var url = 'http://duproprio.com/search/?hash=/g-re=6/s-pmin=0/s-pmax=99999999/s-build=1/s-parent=1/s-filter=sold/s-days=0/m-pack=house-condo/p-con=main/p-ord=date/p-dir=DESC/pa-ge=1/';
    url = nextPage(url);
    /*return scrapMainPage(url).then(function(results) {
        var a = results;
        return results;
    });*/
    for(var i =0; i<5; i++) {
        url = nextPage(url);
        promises.push(scrapMainPage(url).catch(function(error) {
            console.log(error);
        }));
    }
    return Promise.all(promises).then(function(results) {
        writeToFile(_.flatten(results))
    });
}

function writeToFile(results) {
    var text = JSON.stringify((results));
    var saveLocation = path.join(__dirname, 'output2.json');
    fs.writeFile(saveLocation, text, function(err) {
        if(err) {
            return console.log(err);
        }
        console.log("The file was saved!");
    });
}

function findProperty(url) {
    return request.getAsync(url).spread(function(response, html) {
        var $ = cheerio.load(html);
        var feature = findFeatures($);
        var finalPrice = findFinalPrice($);
        var town = findTown($);
        var address = findAddress($);
        var imgs = findImgUrls($);
        return _.merge(
            feature,
            finalPrice,
            town,
            address,
            imgs
        );
    }).catch(function(error) {
        console.log(error);
    });
}


