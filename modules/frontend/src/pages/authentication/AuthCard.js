import PropTypes from "prop-types"

import { Box } from "@mui/material"

// import MainCard from "components/MainCard"

const AuthCard = ({ children, ...other }) => (
  <Box
    sx={{
      maxWidth: { xs: 400, lg: 475 },
      margin: { xs: 2.5, md: 3 },
      "& > *": {
        flexGrow: 1,
        flexBasis: "50%",
      },
    }}
    content={false}
    {...other}
    border={false}
    boxShadow
    shadow={(theme) => theme.customShadows.z1}
  >
    <Box sx={{ p: { xs: 2, sm: 3, md: 4, xl: 5 } }}>{children}</Box>
  </Box>
)

AuthCard.propTypes = {
  children: PropTypes.node,
}

export default AuthCard
