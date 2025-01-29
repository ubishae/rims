import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from './ui/table'
import { Card, CardContent, CardHeader, CardTitle } from './ui/card'

const risks = [
  { id: 1, name: 'Data Breach', probability: 0.3, impact: 0.9, score: 0.27 },
  {
    id: 2,
    name: 'System Downtime',
    probability: 0.5,
    impact: 0.7,
    score: 0.35,
  },
  {
    id: 3,
    name: 'Compliance Violation',
    probability: 0.2,
    impact: 0.8,
    score: 0.16,
  },
  {
    id: 4,
    name: 'Supply Chain Disruption',
    probability: 0.4,
    impact: 0.6,
    score: 0.24,
  },
  {
    id: 5,
    name: 'Reputational Damage',
    probability: 0.3,
    impact: 0.8,
    score: 0.24,
  },
]

export function RiskScoringTable() {
  return (
    <Card>
      <CardHeader>
        <CardTitle>Risk Scoring</CardTitle>
      </CardHeader>
      <CardContent>
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead>Risk</TableHead>
              <TableHead>Probability</TableHead>
              <TableHead>Impact</TableHead>
              <TableHead>Risk Score</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {risks.map((risk) => (
              <TableRow key={risk.id}>
                <TableCell>{risk.name}</TableCell>
                <TableCell>{risk.probability.toFixed(2)}</TableCell>
                <TableCell>{risk.impact.toFixed(2)}</TableCell>
                <TableCell>{risk.score.toFixed(2)}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </CardContent>
    </Card>
  )
}
