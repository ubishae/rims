import { Card, CardContent, CardHeader, CardTitle } from './ui/card'

export function RiskHeatmap({ className }: { className?: string }) {
  return (
    <Card className={className}>
      <CardHeader>
        <CardTitle>Risk Heatmap</CardTitle>
      </CardHeader>
      <CardContent>
        <div className="flex h-[200px] items-center justify-center bg-muted">
          Heatmap Placeholder
        </div>
      </CardContent>
    </Card>
  )
}
