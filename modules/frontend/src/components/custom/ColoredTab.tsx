import { styled } from "@mui/material/styles"
import Tab, { TabProps } from "@mui/material/Tab"

interface ColoredTabProps extends TabProps {
  userColor?: string
}

const ColoredTab = styled(Tab)<ColoredTabProps>(({ theme, userColor }) => ({
  "&.Mui-selected": {
    color: userColor || theme.palette.secondary.main,
    backgroundColor: userColor || theme.palette.secondary.main,
    "&:hover": {
      backgroundColor: userColor || theme.palette.secondary.main,
    },
  },
}))

export default ColoredTab
