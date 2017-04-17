#!/usr/bin/python3
# -*- coding: utf-8 -*-

import json
import requests
from pprint import pprint
import uuid
import time
import urllib3
urllib3.disable_warnings()

url = 'https://localhost:4984/'

pprint("SERVER:")
server = requests.get(url, headers={"Accept": "application/json"}, verify=False)
pprint(server.json())

url = url + "beer/"

pprint("DATABASE:")
database = requests.get(url, headers={"Accept": "application/json"}, verify=False)
pprint(database.json())

pprint("ALL DOCS:")
all_docs = requests.get(url + "_all_docs?include_docs=true", headers={"Accept": "application/json"}, verify=False)
pprint(all_docs.json())
'''
for Nr in range(1,11):
    id = str(uuid.uuid1())
    docid = "beer_" + id
    payload = { 'Id': id, 'beer': "Švyturio Gintarinis", 'taste': True, 'Nr': Nr }
    newdoc = requests.put(url + docid, headers={"Accept": "application/json"}, verify=False, data=json.dumps(payload))
    pprint(newdoc)
    time.sleep(15)
'''
pprint("CHANGES:")
changes = requests.get(url + "_changes?style=main_only&active_only=true&include_docs=true", headers={"Accept": "application/json"}, verify=False)
pprint(changes.json())
'''
pprint("DOC beer_aa1:")
one_doc = requests.get(url + "beer_aa1", headers={"Accept": "application/json"}, verify=False)
pprint(one_doc.json())

new_age = int(one_doc.json()['Age']) + 1 if int(one_doc.json()['Age']) > 100 else 666

pprint("UPDATE beer_aa1:")
payload = { 'Age': new_age, 'Beer': "Švyturio Gintarinis", 'Good': False, '_rev': one_doc.json()['_rev'] }
updated = requests.put(url + "beer_aa1?new_edits=true", headers={"Accept": "application/json"}, verify=False, data=json.dumps(payload))
pprint(updated.json())

pprint("DOC beer_aa1:")
one_doc = requests.get(url + "beer_aa1", headers={"Accept": "application/json"}, verify=False)
pprint(one_doc.json())
'''
for n in range(1,11):
    pprint("UPDATE beer_aa1 [%d]:" % n)
    for change in changes.json()['results']:
        if change['id'] == 'beer_aa1':

            doc = change['doc']
            new_age = int(doc['Age']) + 1 if int(doc['Age']) > 100 else int(doc['Age']) + 100

            payload = { 'Age': new_age, 'Beer': "Švyturio Gintarinis", 'Good': False, '_rev': doc['_rev'] }
            pprint("AAA:")
            pprint(payload)
            updated = requests.put(url + "beer_aa1?new_edits=true", headers={"Accept": "application/json"}, verify=False, data=json.dumps(payload))
            pprint(updated.json())

            pprint("DOC beer_aa1:")
            one_doc = requests.get(url + "beer_aa1", headers={"Accept": "application/json"}, verify=False)
            pprint(one_doc.json())

    time.sleep(15)
