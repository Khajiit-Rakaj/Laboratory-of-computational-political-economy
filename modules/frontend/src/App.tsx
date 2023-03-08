import styles from "./App.module.css"
import { useState } from "react"
import { Routes, Route } from "react-router-dom"
import { CssBaseline, ThemeProvider } from "@mui/material"
import { ColorModeContext, useMode } from "./theme"
import Header from "./components/Header/Header"
import Table from "./components/Table/Table"
import About from "./components/About/About"
import { Login2 } from "./components/Login/Login2"

export const App = () => {
  const [theme, colorMode] = useMode()

  return (
    <ColorModeContext.Provider value={colorMode}>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <div className={styles.app}>
          <Header />
          <main>
            <Routes>
              <Route path="/" element={<Table />} />
              <Route path="/login" element={<Login2 />} />
              <Route path="/about" element={<About />} />
            </Routes>
          </main>
        </div>
      </ThemeProvider>
    </ColorModeContext.Provider>
  )
}
