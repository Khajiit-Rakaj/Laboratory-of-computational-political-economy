import pandas as pd

from dataload.Store import CouchbaseStore
from util.stat_util import heat_map, print_correlations

db = CouchbaseStore()
result = db.query("select cf.* from corporate_finance cf")
df = pd.DataFrame(result)
df = df.mask(df == "")
df = df.convert_dtypes()

list1 = ["подоходный_налог", "зарплата", "переменный_капитал", "траты_на_иссл."]
list2 = ["чистая_выручка", "чистая_прибыль"]

# df = df.dropna(subset=list1 + list2)

corp_name = "corporation_name"

corps = df[corp_name].unique()
data_by_corp = df.groupby([corp_name])

for corp in corps:
    print(corp)
    corp_df = df[df[corp_name] == corp]
    heat_map(corp_df, "{}.png".format(corp))

    for first in list1:
        for second in list2:
            corp_cleansed = corp_df.dropna(subset=[first, second])
            x = corp_cleansed[first]
            y = corp_cleansed[second]
            print("Корр. {} к {}".format(first, second))
            print_correlations(x, y)
