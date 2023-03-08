import React, { useState } from "react"
import {
  Link,
  Container,
  Typography,
  Divider,
  Stack,
  Button,
  Box,
  TextField,
  IconButton,
  InputAdornment,
  Checkbox,
} from "@mui/material"
import VisibilityIcon from "@mui/icons-material/Visibility"

type Login2 = {}

export const Login2 = (): JSX.Element => {
  const [showPassword, setShowPassword] = useState(false)

  return (
    <Container
      sx={{
        width: 480,
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        paddingTop: "100px",
      }}
    >
      <Box flexDirection={"column"} paddingTop={"50px"}>
        <Typography variant="h4" gutterBottom>
          Login
        </Typography>

        <Divider sx={{ my: 3 }} />

        <Box>
          <Stack spacing={3}>
            <TextField name="email" label="Email address" />
            <TextField
              name="password"
              label="Password"
              type={showPassword ? "text" : "password"}
              InputProps={{
                endAdornment: (
                  <InputAdornment position="end">
                    <IconButton onClick={() => setShowPassword(!showPassword)} edge="end">
                      <VisibilityIcon />
                    </IconButton>
                  </InputAdornment>
                ),
              }}
            />
          </Stack>
          <Divider sx={{ my: 3 }} />

          <Button fullWidth size="large" type="submit" variant="contained">
            Login
          </Button>
        </Box>
      </Box>
    </Container>
  )
}
