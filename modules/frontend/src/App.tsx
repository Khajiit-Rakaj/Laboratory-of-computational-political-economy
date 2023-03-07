import styles from "./App.module.css"
import { Routes, Route, Navigate } from "react-router-dom"
import Header from "./components/Header/Header"
import Table from "./components/Table/Table"
import About from "./components/About/About"
import Login from "./components/Login/Login"

export const App = () => {
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
