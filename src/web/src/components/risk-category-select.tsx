import {
  Select,
  SelectContent,
  SelectGroup,
  SelectItem,
  SelectLabel,
  SelectTrigger,
  SelectValue,
} from './ui/select'
import { useGetRiskCategories } from '../hooks/use-risk-category'
import { Skeleton } from './ui/skeleton'

interface RiskCategorySelectProps {
  value?: string
  onValueChange?: (value: string) => void
  placeholder?: string
  label?: string
  disabled?: boolean
}

export function RiskCategorySelect({
  value,
  onValueChange,
  placeholder = 'Select a category',
  label = 'Risk Category',
  disabled,
}: RiskCategorySelectProps) {
  const { data: categories, isLoading, isError } = useGetRiskCategories()

  if (isLoading) {
    return <Skeleton className="h-10 w-full" />
  }

  if (isError) {
    return (
      <Select disabled value="">
        <SelectTrigger className="w-full">
          <SelectValue placeholder="Error loading categories" />
        </SelectTrigger>
      </Select>
    )
  }

  return (
    <Select
      value={value}
      onValueChange={onValueChange}
      disabled={disabled || !categories?.length}
    >
      <SelectTrigger className="w-full">
        <SelectValue placeholder={placeholder} />
      </SelectTrigger>
      <SelectContent>
        <SelectGroup>
          <SelectLabel>{label}</SelectLabel>
          {categories?.map((category) => (
            <SelectItem key={category.id} value={category.id.toString()}>
              {category.name}
            </SelectItem>
          ))}
        </SelectGroup>
      </SelectContent>
    </Select>
  )
}
