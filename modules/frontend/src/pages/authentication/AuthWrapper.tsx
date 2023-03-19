import React from "react"
import PropTypes from "prop-types"

import { Box, Grid } from "@mui/material"

import AuthFooter from "./AuthFooter"

import AuthBackground from "./AuthBackground"

type AuthWrapperProps = {
  children: React.ReactNode
}

const AuthWrapper: React.FC<AuthWrapperProps> = ({ children }) => (
  <Box sx={{ minHeight: "100vh" }}>
    AuthWrapper
    <AuthBackground />
    <Grid
      container
      direction="column"
      justifyContent="flex-start"
      sx={{
        minHeight: "100vh",
      }}
    >
      <Grid item xs={12}>
        <Grid
          item
          xs={12}
          container
          justifyContent="center"
          alignItems="center"
          sx={{ minHeight: { xs: "calc(100vh - 234px)", md: "calc(100vh - 112px)" } }}
        >
          <Grid item>{children}</Grid>
        </Grid>
      </Grid>
      {/* <Grid item xs={12} sx={{ m: 3, mt: 1 }}>
        <AuthFooter />
      </Grid> */}
    </Grid>
  </Box>
)

AuthWrapper.propTypes = {
  children: PropTypes.node,
}

export default AuthWrapper
