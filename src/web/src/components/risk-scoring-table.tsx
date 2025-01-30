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
import { useGetRisks } from '../hooks/use-risk'

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
                </TableRow>
              ))
            )}
          </TableBody>
        </Table>
      </CardContent>
    </Card>
  )
}
