from clarifai.client import ClarifaiApi
import json
import time
from pprint import pprint

## clarifai API KEY
client_id = "1z6E91EP757L7x7PhI5MyGJmyKgL-tBWpCRCW0q5"
secret_id = "N1xX2RDEhfLhmovsoafCS87JCx0EJptJ_iGuOgc4"

## House data config
json_path = r"house_data"
output_file = "modified_data"
images_key = "img_urls"
tokens_key = "tokens"

clarifai_api = ClarifaiApi(client_id, secret_id)

image_load = 0


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
        for result in results["results"]:
            for tag in result["result"]["tag"]["classes"]:
                all_tags.append(tag)
        return list(set(all_tags))
    except Exception as e:
        print("Error while getting tag: {0}".format(e))

def retrieveTagFromObject():
    print("Hello")


def loadFile(path):
    with open(path) as data_file:
        data = json.load(data_file)
    return data


def getTokensFromImages():
    try:
        data = loadFile(json_path)
        for value in data:
            if images_key in value:
                images = value[images_key]
                tags = getTags_debug(images)
                if tags:
                    value[tokens_key] = tags
                else:
                    print("Error with tags")
    except Exception as e:
        print(str(e))
    return data

def writeJsonToFile(data):
    with open(output_file, 'w') as outfile:
        json.dump(data, outfile)

modifiedData = getTokensFromImages()
writeJsonToFile(modifiedData)

print("Nomber of images load {0}".format(image_load))


