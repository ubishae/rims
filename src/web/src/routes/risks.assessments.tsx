import { createFileRoute } from '@tanstack/react-router'
import { DashboardHeader } from '../components/dashboard-header'
import { DashboardShell } from '../components/dashboard-shell'
import { RiskScoringTable } from '../components/risk-scoring-table'
import { ScenarioAnalysis } from '../components/scenario-analysis'
import { RiskAssessmentForm } from '../components/risk-assessment-form'

export const Route = createFileRoute('/risks/assessments')({
  component: RouteComponent,
})

function RouteComponent() {
  return (
    <DashboardShell>
      <DashboardHeader
        heading="Risk Assessment"
        text="Evaluate and analyze risks across your organization."
      />
      <div className="grid gap-6">
        <RiskScoringTable />
        <div className="grid gap-6 md:grid-cols-2">
          <ScenarioAnalysis />
          <RiskAssessmentForm />
        </div>
      </div>
    </DashboardShell>
  )
}
