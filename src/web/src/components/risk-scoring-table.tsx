import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from './ui/table'
import { Card, CardContent, CardHeader, CardTitle } from './ui/card'
import { Skeleton } from './ui/skeleton'
import { useGetRisks } from '@/hooks/use-risk'
import { Badge } from './ui/badge'

const getRiskLevelVariant = (level?: string) => {
  switch (level?.toLowerCase()) {
    case 'negligible':
      return 'info'
    case 'low':
      return 'success'
    case 'medium':
      return 'warning'
    case 'high':
      return 'destructive'
    case 'critical':
      return 'destructive'
    default:
      return 'default'
  }
}

export function RiskScoringTable() {
  const { data: risks, isLoading, isError, error } = useGetRisks()

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
              <TableHead>Score</TableHead>
              <TableHead>Level</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {isLoading ? (
              <>
                {Array.from({ length: 5 }).map((_, index) => (
                  <TableRow key={index}>
                    <TableCell><Skeleton className="h-4 w-[250px]" /></TableCell>
                    <TableCell><Skeleton className="h-4 w-[80px]" /></TableCell>
                    <TableCell><Skeleton className="h-4 w-[80px]" /></TableCell>
                    <TableCell><Skeleton className="h-4 w-[80px]" /></TableCell>
                  </TableRow>
                ))}
              </>
            ) : isError ? (
              <TableRow>
                <TableCell colSpan={4} className="text-center text-red-500">
                  Error loading risks: {error?.message || 'Unknown error'}
                </TableCell>
              </TableRow>
            ) : risks?.length === 0 ? (
              <TableRow>
                <TableCell colSpan={4} className="text-center text-muted-foreground">
                  No risks found
                </TableCell>
              </TableRow>
            ) : (
              risks?.map((risk) => (
                <TableRow key={risk.id}>
                  <TableCell>{risk.title}</TableCell>
                  <TableCell>{risk.probabilityScore?.toFixed(2) || '-'}</TableCell>
                  <TableCell>{risk.impactScore?.toFixed(2) || '-'}</TableCell>
                  <TableCell>{risk.riskScore?.toFixed(2) || '-'}</TableCell>
                  <TableCell><Badge variant={getRiskLevelVariant(risk.level)}>{risk.level || '-'}</Badge></TableCell>
                </TableRow>
              ))
            )}
          </TableBody>
        </Table>
      </CardContent>
    </Card>
  )
}
