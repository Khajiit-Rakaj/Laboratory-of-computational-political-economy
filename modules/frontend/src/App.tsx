import { Routes, Route } from "react-router-dom"
import { CssBaseline, ThemeProvider } from "@mui/material"
import { ColorModeContext, useMode } from "./components/theme/theme"
import Table from "./components/Table/Table"
import About from "./components/About/About"
import Header from "./components/Header/Header"
import Upload from "./components/Upload/Upload"
import Login from "./pages/Login"

export const App = () => {
  const [theme, colorMode] = useMode()

  return (
    <ColorModeContext.Provider value={colorMode}>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <div className="app">
          <Header />
          <Routes>
            <Route path="/" element={<Table />} />
            <Route path="/login" element={<Login />} />
            <Route path="/about" element={<About />} />
            <Route path="/upload" element={<Upload />} />
          </Routes>
        </div>
      </ThemeProvider>
    </ColorModeContext.Provider>
  )
}
