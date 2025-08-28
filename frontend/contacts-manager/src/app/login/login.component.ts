import { Component, inject } from '@angular/core';
import { CinputComponent } from "../components/cinput/cinput.component";
import { FormsModule } from '@angular/forms';
import { CButtonComponent } from "../components/cbutton/cbutton.component";
import { CommonModule } from '@angular/common';
import { AuthService } from '../services/auth-service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: true,
  imports: [CinputComponent, CommonModule, FormsModule, CButtonComponent]
})
export class LoginComponent {
  private readonly authService = inject(AuthService);

  isRegister: boolean = false;

  email: string = '';
  password: string = '';

  name: string = '';
  lastName: string = '';

  toggleForm() {
    this.isRegister = !this.isRegister;
    this.lastName = '';
    this.name = '';
  }

  resetForm(){
    this.email = '';
    this.password = '';
    this.name = '';
    this.lastName = '';
  }

  login(){
    this.authService.login(this.email, this.password).subscribe(
      {
        next: (v) => {
          var token = v;
          console.log(token);
        },
        error: (e) => console.error(e),
        complete: () => console.info('complete')
      }
    );
  }

  register(){
    this.authService.register(this.name, this.lastName, this.email, this.password).subscribe(
      {
        next: (v) => {
          console.log('SUCESSO');
          this.resetForm();
          this.toggleForm();
        },
        error: (e) => console.error(e),
        complete: () => console.info('complete')
      }
    )
  }
}
