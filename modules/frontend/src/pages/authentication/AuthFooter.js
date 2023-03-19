// material-ui
import { useMediaQuery, Container, Link, Typography, Stack } from "@mui/material"

const AuthFooter = () => {
  const matchDownSM = useMediaQuery((theme) => theme.breakpoints.down("sm"))

  return (
    <Container maxWidth="xl">
      <Stack
        direction={matchDownSM ? "column" : "row"}
        justifyContent={matchDownSM ? "center" : "space-between"}
        spacing={2}
        textAlign={matchDownSM ? "center" : "inherit"}
      >
        <Typography variant="subtitle2" color="secondary" component="span">
          Lorem ipsum
          <Typography component={Link} variant="subtitle2" underline="hover">
            Lorem ipsum
          </Typography>
        </Typography>

        <Stack
          direction={matchDownSM ? "column" : "row"}
          spacing={matchDownSM ? 1 : 3}
          textAlign={matchDownSM ? "center" : "inherit"}
        >
          <Typography variant="subtitle2" color="secondary" underline="hover">
            Lorem ipsum
          </Typography>
          <Typography variant="subtitle2" color="secondary" underline="hover">
            Lorem ipsum
          </Typography>
          <Typography variant="subtitle2" color="secondary" underline="hover">
            Lorem ipsum
          </Typography>
        </Stack>
      </Stack>
    </Container>
  )
}

export default AuthFooter
