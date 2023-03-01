import styles from "./App.module.css"
import DATA from "./corp_finance.json"
import Header from "./components/Header/Header"
import Table from "./components/Table/Table"

export const App = () => {
  console.log(DATA)

  return (
    <>
      <Header />
      <div className={styles.app}>
        <main className={styles.content}>
          <h1>Hello</h1>
        </main>
        <Table />
      </div>
    </>
  )
}
