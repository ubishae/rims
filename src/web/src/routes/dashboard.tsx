import { createFileRoute } from '@tanstack/react-router'
import { DashboardHeader } from '../components/dashboard-header'
import { DashboardShell } from '../components/dashboard-shell'
import { KPICards } from '../components/kpi-cards'
import { RecentAlerts } from '../components/recent-alerts'
import { RiskHeatmap } from '../components/risk-heatmap'
import { RiskOverviewCards } from '../components/risk-overview-cards'

export const Route = createFileRoute('/dashboard')({
  component: RouteComponent,
})

function RouteComponent() {
  return (
    <DashboardShell>
      <DashboardHeader
        heading="Dashboard"
        text="Welcome to your Risk Intelligence and Monitoring System Dashboard."
      />
      <div className="grid gap-6 py-6">
        <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-4">
          <RiskOverviewCards />
        </div>
        <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-7">
          <RiskHeatmap className="col-span-4" />
          <RecentAlerts className="col-span-3" />
        </div>
        <div className="py-2">
          <KPICards />
        </div>
      </div>
    </DashboardShell>
  )
}
