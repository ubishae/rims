import { Card, CardContent, CardHeader, CardTitle } from './ui/card'
import { Button } from './ui/button'
import { Input } from './ui/input'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { z } from 'zod'

import { RiskCategorySelect } from './risk-category-select'
import { FormField, FormItem, FormLabel, FormControl, FormDescription, FormMessage, Form } from './ui/form'
import { Textarea } from './ui/textarea'
import { useCreateRisk } from '../hooks/use-risk'


const formSchema = z.object({
  title: z.string(),
  description: z.string(),
  categoryId: z.coerce.number(),
  probabilityScore: z.coerce.number().min(0).max(10),
  impactScore: z.coerce.number().min(0).max(10),
})

type FormInput = z.infer<typeof formSchema>

export function RiskAssessmentForm() {
  const form = useForm<FormInput>({resolver: zodResolver(formSchema), defaultValues: {
    title: '',
    description: '',
    categoryId: 0,
    probabilityScore: 0,
    impactScore: 0,
  }})

  const createRisk = useCreateRisk<FormInput>()

  const onSubmit = (values: FormInput) => {
    console.log(values)
    createRisk.mutate(values)
    form.reset()
  }

  return (
    <Card>
      <CardHeader>
        <CardTitle>Risk Assessment Form</CardTitle>
      </CardHeader>
      <CardContent>
        <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)} className="grid gap-4">
          <div className="grid gap-2">
          <FormField
            control={form.control}
            name="title"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Title</FormLabel>
                <FormControl>
                  <Input {...field} />
                </FormControl>
                <FormDescription>Title of the risk assessment.</FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />
          </div>
          <div className="grid gap-2">
          <FormField
            control={form.control}
            name="description"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Description</FormLabel>
                <FormControl>
                  <Textarea {...field} />
                </FormControl>
                <FormDescription>Description of the risk assessment.</FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />
          </div>
          <div className="grid gap-2">
            <FormField
            control={form.control}
            name="categoryId"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Category</FormLabel>
                <FormControl>
                <RiskCategorySelect onValueChange={field.onChange} />
                </FormControl>
                <FormDescription>Category of the risk.</FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />
            
          </div>
          <div className="grid gap-2">
          <FormField
            control={form.control}
            name="impactScore"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Impact Score</FormLabel>
                <FormControl>
                  <Input type='number' {...field} />
                </FormControl>
                <FormDescription>Impact score of the risk.</FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />
          </div>
          <div className="grid gap-2">
        <FormField
          control={form.control}
          name="probabilityScore"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Probability Score</FormLabel>
              <FormControl>
                <Input type='number' {...field} />
              </FormControl>
              <FormDescription>Probability score of the risk.</FormDescription>
              <FormMessage />
            </FormItem>
          )}
          />
          </div>
          <Button type="submit">Submit Risk Assessment</Button>
        </form>
        </Form>
      </CardContent>
    </Card>
  )
}
