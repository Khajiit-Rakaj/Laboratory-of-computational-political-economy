import {
  Box,
  Typography,
  useTheme,
  Chip,
  ButtonGroup,
  Button,
  AppBar,
  Toolbar,
  IconButton,
} from "@mui/material"
import Form from "../Form/Form"
import { tokens } from "../theme/theme"
import AccessTimeIcon from "@mui/icons-material/AccessTime"
import AccessTimeOutlinedIcon from "@mui/icons-material/AccessTimeOutlined"

const LoginPage = (): JSX.Element => {
  const theme = useTheme()
  const colors = tokens(theme.palette.mode)

  return (
    <Box maxWidth={"400px"} m={"0 auto"} pt={5}>
      <Typography
        fontWeight="bold"
        fontSize="32px"
        color="primary"
        textAlign="center"
        sx={{ padding: "0 0 20px" }}
      >
        SIGN IN
      </Typography>
      <Form />
      <Button variant="contained" color="error">
        Error
      </Button>
      <Typography variant="subtitle1" color="success.main">
        Привет
      </Typography>

      <AccessTimeOutlinedIcon sx={{ fontSize: "160px" }} color="secondary" />
    </Box>
  )
}

export default LoginPage
