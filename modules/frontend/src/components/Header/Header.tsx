import React from "react"
import styles from "./Header.module.css"

function Header() {
  return (
    <header className={styles.header}>
      <div className="narrow">
        <nav>
          <ul>
            <li>
              <a href="/">Home</a>
            </li>
            <li>
              <a href="/login">Login</a>
            </li>
            <li>
              <a href="/about">About</a>
            </li>
          </ul>
        </nav>
      </div>
    </header>
  )
}

export default Header
