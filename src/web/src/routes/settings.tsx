import { createFileRoute } from '@tanstack/react-router'
import { DashboardHeader } from '../components/dashboard-header'
import { DashboardShell } from '../components/dashboard-shell'
import { NotificationSettings } from '../components/notification-settings'
import { UserProfileSettings } from '../components/user-profile-settings'
import { SystemSettings } from '../components/system-settings'

export const Route = createFileRoute('/settings')({
  component: RouteComponent,
})

function RouteComponent() {
  return (
    <DashboardShell>
      <DashboardHeader
        heading="Settings"
        text="Manage your RIMS settings and preferences."
      />
      <div className="grid gap-6">
        <NotificationSettings />
        <UserProfileSettings />
        <SystemSettings />
      </div>
    </DashboardShell>
  )
}
