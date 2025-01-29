import { createFileRoute } from '@tanstack/react-router'
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from '../components/ui/card'
import { LoginForm } from '../components/login-form'
import { useEffect } from 'react'

export const Route = createFileRoute('/')({
  component: RouteComponent,
})

function RouteComponent() {
  useEffect(() => {
    console.log(import.meta.env.VITE_API_URL)
  })

  return (
    <div className="flex min-h-screen items-center justify-center bg-gradient-to-r from-blue-100 to-blue-200">
      <Card className="w-full max-w-md">
        <CardHeader className="text-center">
          <CardTitle className="text-3xl font-bold">Welcome to RIMS</CardTitle>
          <CardDescription>
            Risk Intelligence and Monitoring System
          </CardDescription>
        </CardHeader>
        <CardContent>
          <div className="space-y-6">
            <p className="text-center text-sm text-muted-foreground">
              Log in to access your risk management dashboard and real-time
              alerts.
            </p>
            <LoginForm />
          </div>
        </CardContent>
      </Card>
    </div>
  )
}
