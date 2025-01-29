import { Heading } from './ui/heading'
import { Text } from './ui/text'

interface DashboardHeaderProps {
  heading: string
  text?: string
}

export function DashboardHeader({ heading, text }: DashboardHeaderProps) {
  return (
    <div className="flex items-center justify-between px-2">
      <div className="grid gap-1">
        <Heading>{heading}</Heading>
        {text && <Text>{text}</Text>}
      </div>
    </div>
  )
}
