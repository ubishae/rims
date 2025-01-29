'use client'

import { useState } from 'react'
import { Card, CardContent, CardHeader, CardTitle } from './ui/card'
import { Button } from './ui/button'
import { Input } from './ui/input'
import { Label } from './ui/label'

export function ScenarioAnalysis() {
  const [scenario, setScenario] = useState('')
  const [probability, setProbability] = useState('')
  const [impact, setImpact] = useState('')
  const [result, setResult] = useState<string | null>(null)

  const handleAnalysis = () => {
    const probabilityValue = Number.parseFloat(probability)
    const impactValue = Number.parseFloat(impact)

    if (isNaN(probabilityValue) || isNaN(impactValue)) {
      setResult('Please enter valid numbers for probability and impact.')
      return
    }

    const riskScore = probabilityValue * impactValue
    setResult(`The risk score for "${scenario}" is ${riskScore.toFixed(2)}.`)
  }

  return (
    <Card>
      <CardHeader>
        <CardTitle>Scenario Analysis</CardTitle>
      </CardHeader>
      <CardContent>
        <div className="grid gap-4">
          <div className="grid gap-2">
            <Label htmlFor="scenario">Scenario</Label>
            <Input
              id="scenario"
              value={scenario}
              onChange={(e) => setScenario(e.target.value)}
              placeholder="Describe the risk scenario"
            />
          </div>
          <div className="grid gap-2">
            <Label htmlFor="probability">Probability (0-1)</Label>
            <Input
              id="probability"
              type="number"
              min="0"
              max="1"
              step="0.01"
              value={probability}
              onChange={(e) => setProbability(e.target.value)}
            />
          </div>
          <div className="grid gap-2">
            <Label htmlFor="impact">Impact (0-1)</Label>
            <Input
              id="impact"
              type="number"
              min="0"
              max="1"
              step="0.01"
              value={impact}
              onChange={(e) => setImpact(e.target.value)}
            />
          </div>
          <Button onClick={handleAnalysis}>Analyze Scenario</Button>
          {result && <p className="mt-4 text-sm">{result}</p>}
        </div>
      </CardContent>
    </Card>
  )
}
