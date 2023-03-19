// material-ui
import { Box } from "@mui/material"
import Karl from "../../assets/pure Karl.png"

// ==============================|| AUTH BLUR BACK SVG ||============================== //

const AuthBackground = () => {
  return (
    <Box
      sx={(theme) => ({
        position: "absolute",
        filter: "blur(6px)",
        zIndex: -1,
        bottom: 0,
        left: 0,
        // [theme.breakpoints.up("md")]: {
        //   maxWidth: "35%",
        // },
        maxWidth: { md: "35%" },
      })}
      width="50%"
    >
      <img src={Karl} alt="Karl photo" height="100%" width="100%" />
    </Box>
  )
}

export default AuthBackground
