import React from "react"
import AppBar from "@mui/material/AppBar"
import Toolbar from "@mui/material/Toolbar"
import NavTabs from "../NavTabs/NavTabs"

export const Header = (): JSX.Element => {
  return (
    <>
      <AppBar
        elevation={10}
        position="sticky"
        enableColorOnDark
        sx={{ height: "var( --navbar-height)" }}
      >
        <Toolbar>
          <NavTabs />
        </Toolbar>
      </AppBar>
    </>
  )
}
