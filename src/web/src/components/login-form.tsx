import { useNavigate } from '@tanstack/react-router'
import { useState } from 'react'
import { Label } from './ui/label'
import { Button } from './ui/button'
import { Input } from './ui/input'
import { Loader } from 'lucide-react'

export function LoginForm() {
  const [isLoading, setIsLoading] = useState(false)
  const navigate = useNavigate()

  async function onSubmit(event: React.SyntheticEvent) {
    event.preventDefault()
    setIsLoading(true)

    setTimeout(() => {
      setIsLoading(false)
      navigate({ to: '/dashboard' })
    }, 3000)
  }

  return (
    <form onSubmit={onSubmit} className="space-y-4">
      <div className="space-y-2">
        <Label htmlFor="email">Email</Label>
        <Input
          id="email"
          placeholder="name@example.com"
          required
          type="email"
        />
      </div>
      <div className="space-y-2">
        <Label htmlFor="password">Password</Label>
        <Input id="password" required type="password" />
      </div>
      <Button className="w-full" type="submit" disabled={isLoading}>
        {isLoading && <Loader className="mr-2 h-4 w-4 animate-spin" />}
        Sign In
      </Button>
    </form>
  )
}
