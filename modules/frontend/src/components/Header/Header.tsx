import styles from "./Header.module.css"
import { Link } from "react-router-dom"
import { useContext } from "react"
import { Box, IconButton, Button, useTheme } from "@mui/material"
import { ColorModeContext, tokens } from "../../theme"
import LightModeOutlinedIcon from "@mui/icons-material/LightModeOutlined"
import DarkModeOutlinedIcon from "@mui/icons-material/DarkModeOutlined"

function Header(): JSX.Element {
  const theme = useTheme()
  const colors = tokens(theme.palette.mode)
  const colorMode = useContext(ColorModeContext)

  return (
    <Box display="flex" justifyContent="space-between" p={2} component="header">
      <Box component="nav" color="yellowgreen">
        <Link to="/">
          <Button>Home</Button>
        </Link>
        <Link to="/login">
          <Button>Login</Button>
        </Link>
        <Link to="/about">
          <Button>About</Button>
        </Link>
      </Box>
      <IconButton onClick={colorMode.toggleColorMode}>
        {theme.palette.mode === "dark" ? (
          <DarkModeOutlinedIcon />
        ) : (
          <LightModeOutlinedIcon />
        )}
      </IconButton>
    </Box>
  )
}

export default Header
