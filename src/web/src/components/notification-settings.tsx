'use client'

import { useState } from 'react'
import { Card, CardContent, CardHeader, CardTitle } from './ui/card'
import { Label } from './ui/label'
import { Switch } from './ui/switch'
import { Button } from './ui/button'

export function NotificationSettings() {
  const [emailAlerts, setEmailAlerts] = useState(true)
  const [smsAlerts, setSmsAlerts] = useState(false)
  const [pushNotifications, setPushNotifications] = useState(true)

  const handleSave = () => {
    // Implement save functionality here
    console.log('Saving notification settings:', {
      emailAlerts,
      smsAlerts,
      pushNotifications,
    })
  }

  return (
    <Card>
      <CardHeader>
        <CardTitle>Notification Settings</CardTitle>
      </CardHeader>
      <CardContent className="grid gap-6">
        <div className="flex items-center justify-between">
          <Label htmlFor="email-alerts">Email Alerts</Label>
          <Switch
            id="email-alerts"
            checked={emailAlerts}
            onCheckedChange={setEmailAlerts}
          />
        </div>
        <div className="flex items-center justify-between">
          <Label htmlFor="sms-alerts">SMS Alerts</Label>
          <Switch
            id="sms-alerts"
            checked={smsAlerts}
            onCheckedChange={setSmsAlerts}
          />
        </div>
        <div className="flex items-center justify-between">
          <Label htmlFor="push-notifications">Push Notifications</Label>
          <Switch
            id="push-notifications"
            checked={pushNotifications}
            onCheckedChange={setPushNotifications}
          />
        </div>
        <Button onClick={handleSave}>Save Notification Settings</Button>
      </CardContent>
    </Card>
  )
}
