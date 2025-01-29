import { Card, CardContent, CardHeader, CardTitle } from './ui/card'

const recentAlerts = [
  { id: 1, risk: 'Data Breach', severity: 'High', timestamp: '2 hours ago' },
  {
    id: 2,
    risk: 'System Downtime',
    severity: 'Medium',
    timestamp: '5 hours ago',
  },
  { id: 3, risk: 'Compliance Issue', severity: 'Low', timestamp: '1 day ago' },
]

export function RecentAlerts({ className }: { className?: string }) {
  return (
    <Card className={className}>
      <CardHeader>
        <CardTitle>Recent Alerts</CardTitle>
      </CardHeader>
      <CardContent>
        <ul className="space-y-4">
          {recentAlerts.map((alert) => (
            <li key={alert.id} className="flex items-center">
              <span
                className={`mr-2 h-2 w-2 rounded-full ${
                  alert.severity === 'High'
                    ? 'bg-red-500'
                    : alert.severity === 'Medium'
                      ? 'bg-yellow-500'
                      : 'bg-green-500'
                }`}
              ></span>
              <div className="flex-1">
                <p className="text-sm font-medium">{alert.risk}</p>
                <p className="text-xs text-muted-foreground">
                  {alert.timestamp}
                </p>
              </div>
              <span
                className={`text-xs font-medium ${
                  alert.severity === 'High'
                    ? 'text-red-500'
                    : alert.severity === 'Medium'
                      ? 'text-yellow-500'
                      : 'text-green-500'
                }`}
              >
                {alert.severity}
              </span>
            </li>
          ))}
        </ul>
      </CardContent>
    </Card>
  )
}
