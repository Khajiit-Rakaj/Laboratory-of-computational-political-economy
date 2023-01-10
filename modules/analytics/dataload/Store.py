from datetime import timedelta


from couchbase.auth import PasswordAuthenticator
from couchbase.cluster import Cluster
from couchbase.options import ClusterOptions


class CouchbaseStore:
    def __init__(self):
        username = "politecon"

        password = "politecon"
        bucket_name = "politecon"
        scope_name = "_default"

        auth = PasswordAuthenticator(
            username,
            password,
        )

        self.cluster = Cluster('couchbase://localhost', ClusterOptions(auth))
        self.cluster.wait_until_ready(timedelta(seconds=5))

        self.bucket = self.cluster.bucket(bucket_name)
        self.scope = self.bucket.scope(scope_name)

    def query(self, query: str):
        rows = self.scope.query(query)
        results = [row for row in rows]
        return results
