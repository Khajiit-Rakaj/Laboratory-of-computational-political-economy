import styles from "./App.module.css"
import { Routes, Route, Navigate } from "react-router-dom"
import DATA from "./corp_finance.json"
import Header from "./components/Header/Header"
import Table from "./components/Table/Table"
import About from "./components/About/About"
import Login from "./components/Login/Login"

export const App = () => {
  console.log(DATA)

  return (
    <div className={styles.app}>
      <Header />
      <Routes>
        <Route path="/" element={<Table />} />
        <Route path="/login" element={<Login />} />
        <Route path="/about" element={<About />} />
      </Routes>
    </div>
  )
}
//  ;<div className={styles.app}>
//    <main className={styles.content}>
//      <h1>Hello</h1>
//    </main>
//    <Table />
//  </div>
