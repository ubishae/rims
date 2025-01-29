import { Card, CardHeader, CardTitle, CardContent } from './ui/card'

const riskOverviews = [
  { title: 'High Risks', value: '12', change: '+2', changeType: 'negative' },
  { title: 'Medium Risks', value: '24', change: '-3', changeType: 'positive' },
  { title: 'Low Risks', value: '64', change: '+5', changeType: 'neutral' },
  { title: 'Total Risks', value: '100', change: '+4', changeType: 'neutral' },
]

export function RiskOverviewCards() {
  return (
    <>
      {riskOverviews.map((item) => (
        <Card key={item.title}>
          <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
            <CardTitle className="text-sm font-medium">{item.title}</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold">{item.value}</div>
            <p
              className={`text-xs ${item.changeType === 'negative' ? 'text-red-600' : item.changeType === 'positive' ? 'text-green-600' : 'text-muted-foreground'}`}
            >
              {item.change} from last month
            </p>
          </CardContent>
        </Card>
      ))}
    </>
  )
}
