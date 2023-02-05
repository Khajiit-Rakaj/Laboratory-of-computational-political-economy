import matplotlib.pyplot as plt
import pandas as pd
import scipy.stats as st
import seaborn as sns

from dataload.Store import CouchbaseStore

db = CouchbaseStore()
df = pd.DataFrame(db.query("select cf.* from corporate_finance cf"))
df = df.mask(df == "")
df = df.convert_dtypes()

gb = df.groupby(["corporation_name"])


def old_stuff(df, gb):
    print(gb[["gross_profit", "income_tax_expenses"]].corr(method="pearson"))
    print(gb[["gross_profit", "income_tax_expenses"]].corr(method="spearman"))
    print(gb[["gross_profit", "income_tax_expenses"]].corr(method="kendall"))
    print("------------------\n")
    corps = df["corporation_name"].unique()
    df = df.dropna(subset=["gross_profit", "income_tax_expenses"])
    for corp in corps:
        print(corp)
        x = df[df["corporation_name"] == corp]["gross_profit"]
        y = df[df["corporation_name"] == corp]["income_tax_expenses"]

        if x.size > 2:
            c, p = st.pearsonr(x, y)
            print("Pearson: corr={} p={}".format(c, p))
            c, p = st.stats.spearmanr(x, y)
            print("Spearman: corr={} p={}".format(c, p))
            c, p = st.kendalltau(x, y)
            print("Kendall: corr={} p={}".format(c, p))
    corr = df["gross_profit"].astype("float64").corr(df["income_tax_expenses"].astype("float64"))
    print("Correlation {}".format(corr))
    df_corr = df.corr()
    print(df_corr)
    plt.figure(figsize=(12, 9))
    ax = sns.heatmap(df_corr, annot=True, linewidths=1, annot_kws={'size': 7})
    ax.figure.subplots_adjust(left=0.3, bottom=0.3)
    plt.show()


