import { useState } from 'react'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from './ui/table'
import { Button } from './ui/button'
import { Badge } from './ui/badge'
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from './ui/dropdown-menu'
import { MoreHorizontal, CheckCircle, XCircle } from 'lucide-react'

// Sample data - in a real application, this would come from an API
const initialAlerts = [
  {
    id: 1,
    risk: 'Data Breach',
    severity: 'High',
    timestamp: '2023-06-01T10:00:00Z',
    status: 'Open',
  },
  {
    id: 2,
    risk: 'System Downtime',
    severity: 'Medium',
    timestamp: '2023-06-02T14:30:00Z',
    status: 'Open',
  },
  {
    id: 3,
    risk: 'Compliance Violation',
    severity: 'Low',
    timestamp: '2023-06-03T09:15:00Z',
    status: 'Closed',
  },
  {
    id: 4,
    risk: 'Unauthorized Access',
    severity: 'High',
    timestamp: '2023-06-04T16:45:00Z',
    status: 'Open',
  },
  {
    id: 5,
    risk: 'Financial Irregularity',
    severity: 'Medium',
    timestamp: '2023-06-05T11:20:00Z',
    status: 'Closed',
  },
]

export function AlertsTable() {
  const [alerts, setAlerts] = useState(initialAlerts)

  const handleStatusChange = (id: number, newStatus: string) => {
    setAlerts(
      alerts.map((alert) =>
        alert.id === id ? { ...alert, status: newStatus } : alert,
      ),
    )
  }

  return (
    <Table>
      <TableHeader>
        <TableRow>
          <TableHead>Risk</TableHead>
          <TableHead>Severity</TableHead>
          <TableHead>Timestamp</TableHead>
          <TableHead>Status</TableHead>
          <TableHead>Actions</TableHead>
        </TableRow>
      </TableHeader>
      <TableBody>
        {alerts.map((alert) => (
          <TableRow key={alert.id}>
            <TableCell>{alert.risk}</TableCell>
            <TableCell>
              <Badge
                variant={
                  alert.severity === 'High'
                    ? 'destructive'
                    : alert.severity === 'Medium'
                      ? 'secondary'
                      : 'default'
                }
              >
                {alert.severity}
              </Badge>
            </TableCell>
            <TableCell>{new Date(alert.timestamp).toLocaleString()}</TableCell>
            <TableCell>
              <Badge
                variant={alert.status === 'Open' ? 'outline' : 'secondary'}
              >
                {alert.status}
              </Badge>
            </TableCell>
            <TableCell>
              <DropdownMenu>
                <DropdownMenuTrigger asChild>
                  <Button variant="ghost" className="h-8 w-8 p-0">
                    <MoreHorizontal className="h-4 w-4" />
                  </Button>
                </DropdownMenuTrigger>
                <DropdownMenuContent align="end">
                  <DropdownMenuItem
                    onClick={() => handleStatusChange(alert.id, 'Closed')}
                  >
                    <CheckCircle className="mr-2 h-4 w-4" />
                    <span>Mark as Closed</span>
                  </DropdownMenuItem>
                  <DropdownMenuItem
                    onClick={() => handleStatusChange(alert.id, 'Open')}
                  >
                    <XCircle className="mr-2 h-4 w-4" />
                    <span>Reopen Alert</span>
                  </DropdownMenuItem>
                </DropdownMenuContent>
              </DropdownMenu>
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  )
}
