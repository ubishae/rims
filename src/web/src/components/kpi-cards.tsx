import { Card, CardContent, CardHeader, CardTitle } from './ui/card'

const kpis = [
  {
    title: 'Risk Mitigation Rate',
    value: '85%',
    change: '+5%',
    changeType: 'positive',
  },
  {
    title: 'Avg. Response Time',
    value: '2.5 hrs',
    change: '-0.5 hrs',
    changeType: 'positive',
  },
  {
    title: 'Compliance Score',
    value: '92/100',
    change: '+3',
    changeType: 'positive',
  },
]

export function KPICards() {
  return (
    <div className="grid gap-4 md:grid-cols-3">
      {kpis.map((kpi) => (
        <Card key={kpi.title}>
          <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
            <CardTitle className="text-sm font-medium">{kpi.title}</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold">{kpi.value}</div>
            <p
              className={`text-xs ${kpi.changeType === 'positive' ? 'text-green-600' : 'text-red-600'}`}
            >
              {kpi.change} from last month
            </p>
          </CardContent>
        </Card>
      ))}
    </div>
  )
}
