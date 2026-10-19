import { Component } from '@angular/core';

/**
 * Početna (landing) stranica javnog dijela aplikacije.
 * Ruta: '' unutar PublicLayoutComponent (vidi public-routing-module.ts).
 */
@Component({
  selector: 'app-landing',
  standalone: false,
  templateUrl: './landing.component.html',
  styleUrl: './landing.component.scss',
})
export class LandingComponent {}
