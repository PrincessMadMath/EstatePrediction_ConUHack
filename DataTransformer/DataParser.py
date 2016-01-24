from clarifai.client import ClarifaiApi
import json
import time
import random
from pprint import pprint

## clarifai API KEY
# Felix
#client_id = "1z6E91EP757L7x7PhI5MyGJmyKgL-tBWpCRCW0q5"
#secret_id = "N1xX2RDEhfLhmovsoafCS87JCx0EJptJ_iGuOgc4"

# Sam
client_id = "0SpA6Z98Ccaghh8G-hdyVEpmkWPwBJwKrxGAsYPZ"
secret_id = "R4n5OaCPBknohho1RWL8HPdJ3KyPI-KgThCIQ0w6"

## House data config
json_path = r"output0.json"
output_file = "modified_data_2"
images_key = "img_urls"
tokens_key = "tokens"

## Interesting tokens
cool_tokens = ["garage", "porch", "modern", "patio", "environment",  "classic", "contemporary",
               "mansion", "yard", "minimalist", "flora", "parquet", "interior design", "comfort",
               "gazebo", "pavement", "luxury", "scenic", "fireplace","old", "landscape", "barn",
               "swimming pool", "hotel", "pool", "villa", "architecture", "suburb", "travel" ]


## Init
clarifai_api = ClarifaiApi(client_id, secret_id)
image_load = 0
max_image_test = 9950
is_test = True
start_index = 248
sample_size = 8


def getTags_debug(url):
    global image_load
    count = len(url)
    image_load += count
    print("Getting tags for {0} images".format(count))
    start_time = time.time()
    tags = getTags(url)
    print("--- %.2f seconds ---" % (time.time() - start_time))
    return tags

def  getTags(url):
    try:
        results = clarifai_api.tag_image_urls(url)
        all_tags = []
        if results:
            for result in results["results"]:
                for tag in result["result"]["tag"]["classes"]:
                    all_tags.append(tag)
        return set(all_tags)
    except Exception as e:
        print("Error while getting tag: {0}".format(e))

def retrieveTagFromObject():
    print("Hello")


def loadFile(path):
    with open(path) as data_file:
        data = json.load(data_file)
    return data


def getTokensFromImages():
    newData = []
    try:

        data = loadFile(json_path)
        global start_index
        fix_start_index = start_index
        dataSize = len(data)

        for value in data[fix_start_index:]:
            print("Working with house {0}/{1}".format(start_index+1, dataSize))

            start_index = start_index + 1
            if is_test and image_load < max_image_test :
                if images_key in value:
                    images = value[images_key]
                    sample_limite = min(len(images), sample_size)
                    images = random.sample(images,sample_limite)
                    tags = getTags(images)
                    value.pop(images_key, None)
                    for cool_token in cool_tokens:
                        if cool_token in tags:
                            value[cool_token] = 1
                        else:
                            value[cool_token] = 0
                    newData.append(value)
                else:
                    print("Error with tags")

    except Exception as e:
        print(str(e))
    return newData

def writeJsonToFile(data):
    with open(output_file, 'w') as outfile:
        json.dump(data, outfile)

modifiedData = getTokensFromImages()
writeJsonToFile(modifiedData)

print("Nomber of images load {0}".format(image_load))
print("Start index: {0}".format(start_index))


