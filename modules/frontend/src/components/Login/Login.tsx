import styles from "./Login.module.css"
import React, { FormEvent } from "react"

type FormData = {
  email: string
  password: string
}

const getFormValues = (form: HTMLFormElement): { isEmpty: boolean; data: FormData } => {
  const formData = new FormData(form)

  const values = [...formData.values()]
  const isEmpty = values.includes("")
  const data = Object.fromEntries(formData) as unknown as FormData
  return { isEmpty, data }
}

const Login = (): JSX.Element => {
  const onSubmit = (event: FormEvent<HTMLFormElement>): void => {
    event.preventDefault()
    const { isEmpty, data } = getFormValues(event.currentTarget)

    if (isEmpty) {
      console.log("Заполните все поля")
      return
    }

    console.log(data)
    event.currentTarget.reset()
  }

  return (
    <section className={styles.login}>
      <form className={styles.form} onSubmit={onSubmit}>
        <div className={styles.row}>
          <label htmlFor="email" className={styles.label}>
            Email
          </label>
          <input id="email" type="email" name="email" className={styles.input} />
        </div>
        <div className={styles.row}>
          <label htmlFor="password" className={styles.label}>
            Password
          </label>
          <input id="password" type="password" name="password" className={styles.input} />
        </div>
        <button type="submit" className={styles.button}>
          Login
        </button>
      </form>
    </section>
  )
}

export default Login
