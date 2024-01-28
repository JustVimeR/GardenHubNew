import { Component } from '@angular/core';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.scss']
})
export class NotificationsComponent {
  fakeNotifications = [
    {
      title: 'Покосити газон на прибудинковій території та обробити від бур’янів',
      username: 'anton___one'
    },
    {
      title: 'Діагностика та лікування троянд',
      username: 'olexandr_shevchenko'
    },
    {
      title: 'Діагностика та лікування троянд',
      username: 'kateryna.lero'
    }
  ]
}
