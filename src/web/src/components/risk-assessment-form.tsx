'use client'

import { useState } from 'react'
import { Card, CardContent, CardHeader, CardTitle } from './ui/card'
import { Button } from './ui/button'
import { Input } from './ui/input'
import { Label } from './ui/label'
import { Textarea } from './ui/textarea'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from './ui/select'

export function RiskAssessmentForm() {
  const [riskName, setRiskName] = useState('')
  const [description, setDescription] = useState('')
  const [category, setCategory] = useState('')
  const [probability, setProbability] = useState('')
  const [impact, setImpact] = useState('')

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault()
    // Here you would typically send this data to your backend
    console.log({ riskName, description, category, probability, impact })
    // Reset form or show success message
  }

  return (
    <Card>
      <CardHeader>
        <CardTitle>Risk Assessment Form</CardTitle>
      </CardHeader>
      <CardContent>
        <form onSubmit={handleSubmit} className="grid gap-4">
          <div className="grid gap-2">
            <Label htmlFor="risk-name">Risk Name</Label>
            <Input
              id="risk-name"
              value={riskName}
              onChange={(e) => setRiskName(e.target.value)}
              required
            />
          </div>
          <div className="grid gap-2">
            <Label htmlFor="description">Description</Label>
            <Textarea
              id="description"
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              required
            />
          </div>
          <div className="grid gap-2">
            <Label htmlFor="category">Category</Label>
            <Select value={category} onValueChange={setCategory} required>
              <SelectTrigger id="category">
                <SelectValue placeholder="Select category" />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value="financial">Financial</SelectItem>
                <SelectItem value="operational">Operational</SelectItem>
                <SelectItem value="strategic">Strategic</SelectItem>
                <SelectItem value="compliance">Compliance</SelectItem>
              </SelectContent>
            </Select>
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
              required
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
              required
            />
          </div>
          <Button type="submit">Submit Risk Assessment</Button>
        </form>
      </CardContent>
    </Card>
  )
}
