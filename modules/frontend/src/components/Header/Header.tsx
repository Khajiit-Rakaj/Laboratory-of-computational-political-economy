import styles from "./Header.module.css"
import { Link } from "react-router-dom"

function Header(): JSX.Element {
  return (
    <header className={styles.header}>
      <div className="narrow">
        <nav>
          <ul>
            <li>
              <Link to="/">Home</Link>
            </li>
            <li>
              <Link to="/login">Login</Link>
            </li>
            <li>
              <Link to="/about">About</Link>
            </li>
          </ul>
        </nav>
      </div>
    </header>
  )
}

export default Header
