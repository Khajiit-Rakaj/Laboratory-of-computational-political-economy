import React, { useState, useEffect } from "react"
import { Link, useLocation } from "react-router-dom"
import Tabs from "@mui/material/Tabs"
import Tab from "@mui/material/Tab"
import { Box, Button, Typography, Slider, Grid } from "@mui/material"

type PathToValueMap = { [key: string]: number }

const NavTabs = () => {
  const [value, setValue] = useState<number>(0)
  const { pathname } = useLocation()

  const onChangeTab = (e: React.SyntheticEvent, value: number) => {
    setValue(value)
  }

  const styleTab = {
    fontSize: "18px",
    fontWeight: "400",
    color: "#FFFBE6",
    // color: (theme:any) => theme.palette.primary.light,
  } as const

  const anotherStyle = {
    visibility: "visible",
    my: 2,
    p: 1,
    bgcolor: (theme: any) => (theme.palette.mode === "dark" ? "#101010" : "grey.100"),
    color: (theme: any) => (theme.palette.mode === "dark" ? "grey.300" : "grey.800"),
    border: "1px solid",
    borderColor: (theme: any) =>
      theme.palette.mode === "dark" ? "grey.800" : "grey.300",
    borderRadius: 2,
    fontSize: "0.875rem",
    fontWeight: "700",
  }

  useEffect(() => {
    const pathToValueMap: PathToValueMap = { "/": 0, "/about": 1, "/login": 2 }

    setValue(pathToValueMap[pathname])
  }, [pathname, value])

  return (
    <Box>
      <Tabs
        value={value}
        onChange={onChangeTab}
        indicatorColor="secondary"
        textColor="secondary"
      >
        <Tab label="Home" component={Link} to="/" sx={styleTab} />
        <Tab label="About" component={Link} to="/about" sx={styleTab} />
        <Tab label="Login" component={Link} to="/login" sx={styleTab} />
      </Tabs>
    </Box>
  )
}

export default NavTabs
