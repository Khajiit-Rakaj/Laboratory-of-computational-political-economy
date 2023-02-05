import matplotlib.pyplot as plt
import scipy.stats as st
import seaborn as sns
from pandas import DataFrame


def print_correlations(x, y):
    if x.size > 2 and y.size > 2:
        c, p = st.pearsonr(x, y)
        print("Pearson: corr={} p={}".format(c, p))
        c, p = st.stats.spearmanr(x, y)
        print("Spearman: corr={} p={}".format(c, p))
        c, p = st.kendalltau(x, y)
        print("Kendall: corr={} p={}".format(c, p))


def heat_map(df: DataFrame, file_name: str):
    df_corr = df.corr()
    plt.figure(figsize=(17, 12))
    ax = sns.heatmap(df_corr, annot=True, linewidths=0.5, annot_kws={'size': 7})
    ax.figure.subplots_adjust(left=0.3, bottom=0.3)
    plt.savefig(file_name)
    plt.show()
