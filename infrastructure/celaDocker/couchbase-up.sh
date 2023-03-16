set -m

/entrypoint.sh couchbase-server &

sleep 15

# https://docs.couchbase.com/server/current/rest-api/rest-intro.html

curl -v -X POST http://127.0.0.1:8091/pools/default -d memoryQuota=512 -d indexMemoryQuota=512

curl -v http://127.0.0.1:8091/node/controller/setupServices -d services=kv%2cn1ql%2Cindex

curl -v http://127.0.0.1:8091/settings/web -d port=8091 -d username=Admin -d password=rootPass

echo "\n\nSetting indices\n"

curl -i -u Admin:rootPass -X POST http://127.0.0.1:8091/settings/indexes -d 'storageMode=forestdb'

echo "\n\nSetting buckets\n"

curl -v -u Admin:rootPass -X POST http://127.0.0.1:8091/pools/default/buckets -d name=politecon -d bucketType=couchbase -d ramQuotaMB=128

echo "\n\nSetting collections\n"

curl -v -u Admin:rootPass -X POST http://127.0.0.1:8091/pools/default/buckets/politecon/scopes/_default/collections -d name=commodity_data
curl -v -u Admin:rootPass -X POST http://127.0.0.1:8091/pools/default/buckets/politecon/scopes/_default/collections -d name=population_data
curl -v -u Admin:rootPass -X POST http://127.0.0.1:8091/pools/default/buckets/politecon/scopes/_default/collections -d name=corporate_finance
curl -v -u Admin:rootPass -X POST http://127.0.0.1:8091/pools/default/buckets/politecon/scopes/_default/collections -d name=economics
curl -v -u Admin:rootPass -X POST http://127.0.0.1:8091/pools/default/buckets/politecon/scopes/_default/collections -d name=patents

curl -v -u Admin:rootPass -X GET http://127.0.0.1:8091/pools/default/buckets/politecon/scopes/

echo "\n\nCreate local user\n"

curl -i -u Admin:rootPass -X PUT http://127.0.0.1:8091/settings/rbac/users/local/politecon -d password=politecon -d roles=bucket_full_access[politecon]

fg %1
