import { createFileRoute } from '@tanstack/react-router'
import { DashboardHeader } from '../components/dashboard-header'
import { DashboardShell } from '../components/dashboard-shell'
import { AlertFilters } from '../components/alert-filters'
import { AlertsTable } from '../components/alerts-table'

export const Route = createFileRoute('/alerts')({
  component: RouteComponent,
})

function RouteComponent() {
  return (
    <DashboardShell>
      <DashboardHeader
        heading="Alerts"
        text="View and manage real-time risk alerts across your organization."
      />
      <div className="grid gap-6">
        <AlertFilters />
        <AlertsTable />
      </div>
    </DashboardShell>
  )
}
