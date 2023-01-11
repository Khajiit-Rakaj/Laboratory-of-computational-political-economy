import matplotlib.pyplot as plt
import pandas as pd
import seaborn as sns

from dataload.Store import CouchbaseStore

db = CouchbaseStore()
df = pd.DataFrame(db.query("select cf.* from corporate_finance cf"))
df = df.mask(df == "")
df = df.convert_dtypes()

gb = df.groupby(["corporation_name"])


print(gb[["gross_profit", "income_tax_expenses"]].corr(method="pearson"))
print(gb[["gross_profit", "income_tax_expenses"]].corr(method="spearman"))
print(gb[["gross_profit", "income_tax_expenses"]].corr(method="kendall"))
print("------------------\n")

corr = df["gross_profit"].astype("float64").corr(df["income_tax_expenses"].astype("float64"))
print("Correlation {}".format(corr))
df_corr = df.corr()

print(df_corr)
plt.figure(figsize=(12, 9))
ax = sns.heatmap(df_corr, annot=True, linewidths=1, annot_kws={'size': 7})
ax.figure.subplots_adjust(left=0.3, bottom=0.3)
plt.show()
