import styles from "./Navbar.module.css"
import { Link } from "react-router-dom"
import { useContext } from "react"
import { Box, IconButton, Button, Typography, useTheme } from "@mui/material"
import { ColorModeContext, tokens } from "../theme/theme"
import LightModeOutlinedIcon from "@mui/icons-material/LightModeOutlined"
import DarkModeOutlinedIcon from "@mui/icons-material/DarkModeOutlined"

function Navbar(): JSX.Element {
  const theme = useTheme()
  const colors = tokens(theme.palette.mode)
  const colorMode = useContext(ColorModeContext)

  return (
    <Box display="flex" justifyContent="space-between" p={2} component="header">
      <Box component="nav" display={"flex"}>
        <Typography
          // color={colors.redAccent[100]}
          component={Link}
          to="/"
          variant="h4"
          color={colors.grey[100]}
          sx={{ textDecoration: "none", margin: "0 15px 0 0" }}
        >
          Home
        </Typography>
        <Typography
          component={Link}
          to="/login"
          variant="h4"
          color={colors.grey[100]}
          sx={{ textDecoration: "none", margin: "0 15px 0 0" }}
        >
          Login
        </Typography>
        <Typography
          component={Link}
          to="/about"
          variant="h4"
          color={colors.grey[100]}
          sx={{ textDecoration: "none", margin: "0 15px 0 0" }}
        >
          About
        </Typography>
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

export default Navbar
