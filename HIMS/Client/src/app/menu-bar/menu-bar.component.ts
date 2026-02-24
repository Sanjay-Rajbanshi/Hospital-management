import { Component } from '@angular/core';
import { RouterLink, RouterModule } from '@angular/router';

@Component({
  selector: 'menu-bar',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './menu-bar.component.html',
  styleUrl: './menu-bar.component.css'
})
export class MenuBarComponent {
isMenuOpen = false;

toggleMenu(){
this.isMenuOpen = !this.isMenuOpen;
}
closeMenu(){
  this.isMenuOpen =false;
}

}
