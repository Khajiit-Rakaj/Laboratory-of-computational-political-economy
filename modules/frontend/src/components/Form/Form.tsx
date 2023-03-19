import { useState, useContext } from "react"
import { Box, Button, TextField, Divider, Typography, useTheme } from "@mui/material"
import { ColorModeContext, tokens } from "../theme/theme"
import EditOutlinedIcon from "@mui/icons-material/EditOutlined"
import { Formik } from "formik"
import * as yup from "yup"
import { useNavigate } from "react-router-dom"
import FlexBetween from "../FlexBetween/FlexBetween"

const loginSchema = yup.object().shape({
  email: yup.string().email("invalid email").required("required"),
  password: yup.string().required("required"),
})

const initialValuesLogin = {
  email: "",
  password: "",
}

const Form = () => {
  const theme = useTheme()
  const colors = tokens(theme.palette.mode)
  const colorMode = useContext(ColorModeContext)
  const navigate = useNavigate()

  const login = async () => {
    navigate("/")
  }

  const handleFormSubmit = async () => {
    await login()
  }

  return (
    <Formik
      onSubmit={handleFormSubmit}
      initialValues={initialValuesLogin}
      validationSchema={loginSchema}
    >
      {({
        values,
        errors,
        touched,
        handleBlur,
        handleChange,
        handleSubmit,
        setFieldValue,
        resetForm,
      }) => (
        <form onSubmit={handleSubmit}>
          <Box display="grid" gap="30px" gridTemplateColumns="repeat(4, minmax(0, 1fr))">
            <TextField
              label="Email"
              onBlur={handleBlur}
              onChange={handleChange}
              value={values.email}
              name="email"
              error={Boolean(touched.email) && Boolean(errors.email)}
              helperText={touched.email && errors.email}
              sx={{ gridColumn: "span 4" }}
            />
            <TextField
              label="Password"
              type="password"
              onBlur={handleBlur}
              onChange={handleChange}
              value={values.password}
              name="password"
              error={Boolean(touched.password) && Boolean(errors.password)}
              helperText={touched.password && errors.password}
              sx={{ gridColumn: "span 4" }}
            />
          </Box>

          {/* BUTTONS */}
          <Box>
            <Button
              fullWidth
              type="submit"
              sx={{
                m: "2rem 0",
                p: "1rem",
                backgroundColor: colors.primary[100],
                color: colors.grey[900],
                "&:hover": { color: colors.primary[100] },
              }}
            >
              LOGIN
            </Button>
            <Typography
              onClick={() => {
                resetForm()
              }}
              sx={{
                textDecoration: "underline",
                color: colors.primary[100],
                "&:hover": {
                  cursor: "pointer",
                  color: colors.primary[100],
                },
              }}
            ></Typography>
          </Box>
        </form>
      )}
    </Formik>
  )
}

export default Form
