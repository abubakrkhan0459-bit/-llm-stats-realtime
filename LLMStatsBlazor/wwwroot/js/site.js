window.blazorExtensions = {
    initParticles: function () {
        const canvas = document.getElementById('particles');
        if (!canvas) return;

        const ctx = canvas.getContext('2d');
        let particles = [];
        let shapes = [];
        const particleCount = 80;
        let mouseX = 0;
        let mouseY = 0;
        let time = 0;

        document.addEventListener('mousemove', (e) => {
            mouseX = e.clientX;
            mouseY = e.clientY;
        });

        class Particle {
            constructor(canvas) {
                this.canvas = canvas;
                this.reset();
            }

            reset() {
                this.x = Math.random() * this.canvas.width;
                this.y = Math.random() * this.canvas.height;
                this.baseSize = Math.random() * 2.5 + 1;
                this.size = this.baseSize;
                this.speedX = (Math.random() - 0.5) * 0.8;
                this.speedY = (Math.random() - 0.5) * 0.8;
                this.baseOpacity = Math.random() * 0.4 + 0.2;
                this.opacity = this.baseOpacity;
                this.colors = ['#0066cc', '#0080ff', '#00d4ff', '#0099ff'];
                this.color = this.colors[Math.floor(Math.random() * this.colors.length)];
                this.pulseSpeed = Math.random() * 0.02 + 0.01;
                this.pulseOffset = Math.random() * Math.PI * 2;
            }

            update() {
                this.x += this.speedX;
                this.y += this.speedY;

                const dx = mouseX - this.x;
                const dy = mouseY - this.y;
                const distance = Math.sqrt(dx * dx + dy * dy);

                if (distance < 150) {
                    const force = (150 - distance) / 150;
                    this.x -= dx * force * 0.03;
                    this.y -= dy * force * 0.03;
                    this.size = this.baseSize * (1 + force * 0.5);
                    this.opacity = this.baseOpacity * (1 + force * 0.3);
                } else {
                    this.size = this.baseSize;
                    this.opacity = this.baseOpacity * (0.8 + Math.sin(time * this.pulseSpeed + this.pulseOffset) * 0.2);
                }

                if (this.x < 0 || this.x > this.canvas.width) this.speedX *= -1;
                if (this.y < 0 || this.y > this.canvas.height) this.speedY *= -1;
            }

            draw(ctx) {
                ctx.save();
                ctx.globalAlpha = this.opacity;

                ctx.shadowColor = this.color;
                ctx.shadowBlur = 15;
                ctx.shadowOffsetX = 0;
                ctx.shadowOffsetY = 0;

                ctx.beginPath();
                ctx.arc(this.x, this.y, this.size, 0, Math.PI * 2);
                ctx.fillStyle = this.color;
                ctx.fill();

                ctx.restore();
            }
        }

        class FloatingShape {
            constructor(canvas) {
                this.canvas = canvas;
                this.reset();
            }

            reset() {
                this.x = Math.random() * this.canvas.width;
                this.y = Math.random() * this.canvas.height;
                this.size = Math.random() * 40 + 20;
                this.rotation = Math.random() * Math.PI * 2;
                this.rotationSpeed = (Math.random() - 0.5) * 0.01;
                this.speedX = (Math.random() - 0.5) * 0.3;
                this.speedY = (Math.random() - 0.5) * 0.3;
                this.opacity = Math.random() * 0.03 + 0.01;
                this.type = Math.random() > 0.5 ? 'triangle' : 'hexagon';
            }

            update() {
                this.x += this.speedX;
                this.y += this.speedY;
                this.rotation += this.rotationSpeed;

                if (this.x < -this.size) this.x = this.canvas.width + this.size;
                if (this.x > this.canvas.width + this.size) this.x = -this.size;
                if (this.y < -this.size) this.y = this.canvas.height + this.size;
                if (this.y > this.canvas.height + this.size) this.y = -this.size;
            }

            draw(ctx) {
                ctx.save();
                ctx.translate(this.x, this.y);
                ctx.rotate(this.rotation);
                ctx.globalAlpha = this.opacity;
                ctx.strokeStyle = '#0066cc';
                ctx.lineWidth = 1;

                ctx.beginPath();
                if (this.type === 'triangle') {
                    for (let i = 0; i < 3; i++) {
                        const angle = (i * Math.PI * 2 / 3) - Math.PI / 2;
                        const x = Math.cos(angle) * this.size;
                        const y = Math.sin(angle) * this.size;
                        if (i === 0) ctx.moveTo(x, y);
                        else ctx.lineTo(x, y);
                    }
                } else {
                    for (let i = 0; i < 6; i++) {
                        const angle = (i * Math.PI * 2 / 6);
                        const x = Math.cos(angle) * this.size;
                        const y = Math.sin(angle) * this.size;
                        if (i === 0) ctx.moveTo(x, y);
                        else ctx.lineTo(x, y);
                    }
                }
                ctx.closePath();
                ctx.stroke();
                ctx.restore();
            }
        }

        function resize() {
            canvas.width = window.innerWidth;
            canvas.height = window.innerHeight;
        }

        resize();
        window.addEventListener('resize', () => resize());

        for (let i = 0; i < particleCount; i++) {
            particles.push(new Particle(canvas));
        }

        for (let i = 0; i < 8; i++) {
            shapes.push(new FloatingShape(canvas));
        }

        function animate() {
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            time++;

            shapes.forEach(shape => {
                shape.update();
                shape.draw(ctx);
            });

            particles.forEach(particle => {
                particle.update();
                particle.draw(ctx);
            });

            for (let i = 0; i < particles.length; i++) {
                for (let j = i + 1; j < particles.length; j++) {
                    const dx = particles[i].x - particles[j].x;
                    const dy = particles[i].y - particles[j].y;
                    const distance = Math.sqrt(dx * dx + dy * dy);

                    if (distance < 120) {
                        const opacity = (1 - distance / 120) * 0.15;
                        ctx.beginPath();
                        ctx.moveTo(particles[i].x, particles[i].y);
                        ctx.lineTo(particles[j].x, particles[j].y);
                        ctx.strokeStyle = `rgba(0, 102, 204, ${opacity})`;
                        ctx.lineWidth = 0.8;
                        ctx.stroke();
                    }
                }
            }

            const mouseGradient = ctx.createRadialGradient(mouseX, mouseY, 0, mouseX, mouseY, 200);
            mouseGradient.addColorStop(0, 'rgba(0, 212, 255, 0.05)');
            mouseGradient.addColorStop(1, 'rgba(0, 212, 255, 0)');
            ctx.fillStyle = mouseGradient;
            ctx.fillRect(0, 0, canvas.width, canvas.height);

            requestAnimationFrame(animate);
        }

        animate();
    },

    init3DCards: function () {
        const cards = document.querySelectorAll('.card');

        cards.forEach(card => {
            // Create shine element
            const shine = document.createElement('div');
            shine.style.cssText = `
                position: absolute;
                top: 0;
                left: 0;
                right: 0;
                bottom: 0;
                background: linear-gradient(135deg, rgba(255,255,255,0) 0%, rgba(255,255,255,0) 40%, rgba(255,255,255,0.15) 50%, rgba(255,255,255,0) 60%, rgba(255,255,255,0) 100%);
                pointer-events: none;
                opacity: 0;
                transition: opacity 0.3s ease;
                z-index: 10;
                border-radius: inherit;
            `;
            card.style.position = 'relative';
            card.appendChild(shine);

            card.addEventListener('mousemove', (e) => {
                const rect = card.getBoundingClientRect();
                const x = e.clientX - rect.left;
                const y = e.clientY - rect.top;
                const centerX = rect.width / 2;
                const centerY = rect.height / 2;
                
                const rotateX = ((y - centerY) / centerY) * -12;
                const rotateY = ((x - centerX) / centerX) * 12;
                
                // Dynamic shadow based on tilt direction
                const shadowX = (x - centerX) / 10;
                const shadowY = (y - centerY) / 10 + 10;
                const shadowBlur = 25 + Math.abs(rotateX) + Math.abs(rotateY);
                
                card.style.transform = `
                    perspective(1000px) 
                    rotateX(${rotateX}deg) 
                    rotateY(${rotateY}deg) 
                    scale3d(1.02, 1.02, 1.02) 
                    translateZ(20px)
                `;
                
                card.style.boxShadow = `
                    ${shadowX}px ${shadowY}px ${shadowBlur}px rgba(0, 102, 204, 0.25),
                    0 15px 35px rgba(0, 0, 0, 0.1),
                    inset 0 1px 0 rgba(255, 255, 255, 0.6)
                `;
                
                // Move shine gradient based on mouse position
                const shineX = ((x / rect.width) * 100);
                const shineY = ((y / rect.height) * 100);
                shine.style.background = `radial-gradient(circle at ${shineX}% ${shineY}%, rgba(255,255,255,0.3) 0%, rgba(255,255,255,0.1) 30%, transparent 60%)`;
                shine.style.opacity = '1';
            });

            card.addEventListener('mouseenter', () => {
                card.style.transition = 'transform 0.15s ease-out, box-shadow 0.15s ease-out';
            });

            card.addEventListener('mouseleave', () => {
                card.style.transition = 'transform 0.5s ease-out, box-shadow 0.5s ease-out';
                card.style.transform = 'perspective(1000px) rotateX(0deg) rotateY(0deg) scale3d(1, 1, 1) translateZ(0)';
                card.style.boxShadow = '';
                shine.style.opacity = '0';
            });
        });
    },

    initCursorGlow: function () {
        // Check if touch device
        if (window.matchMedia('(pointer: coarse)').matches) return;

        // Create cursor glow element
        const cursorGlow = document.createElement('div');
        cursorGlow.className = 'cursor-glow';
        document.body.appendChild(cursorGlow);

        let mouseX = 0;
        let mouseY = 0;
        let currentX = 0;
        let currentY = 0;

        // Track mouse movement
        document.addEventListener('mousemove', (e) => {
            mouseX = e.clientX;
            mouseY = e.clientY;
        });

        // Smooth animation loop
        function animateCursor() {
            // Smooth follow with easing
            currentX += (mouseX - currentX) * 0.15;
            currentY += (mouseY - currentY) * 0.15;

            cursorGlow.style.left = currentX + 'px';
            cursorGlow.style.top = currentY + 'px';

            requestAnimationFrame(animateCursor);
        }

        animateCursor();

        // Hide cursor glow when mouse leaves window
        document.addEventListener('mouseleave', () => {
            cursorGlow.style.opacity = '0';
        });

        document.addEventListener('mouseenter', () => {
            cursorGlow.style.opacity = '1';
        });

        // Create subtle particle trail on movement (less frequent, more subtle)
        let lastTrail = 0;
        let trailCount = 0;
        document.addEventListener('mousemove', (e) => {
            const now = Date.now();
            // Add trail every 150ms and only every 3rd movement
            if (now - lastTrail > 150 && ++trailCount % 3 === 0) {
                lastTrail = now;
                createTrail(e.clientX, e.clientY);
            }
        });

        function createTrail(x, y) {
            const trail = document.createElement('div');
            trail.className = 'cursor-trail';
            // Random slight offset for natural feel
            const offsetX = (Math.random() - 0.5) * 10;
            const offsetY = (Math.random() - 0.5) * 10;
            trail.style.left = (x + offsetX) + 'px';
            trail.style.top = (y + offsetY) + 'px';
            document.body.appendChild(trail);

            setTimeout(() => {
                trail.remove();
            }, 800);
        }
    },

    initBubbleEffects: function () {
        // Add bubble effect to headings
        document.querySelectorAll('h1, h2, h3, .hero-title h1, .highlights-title, .card-header h2').forEach(el => {
            el.classList.add('bubble-text');
        });

        // Add bubble effect to model names
        document.querySelectorAll('.model-name, .model-company, .os-title, .os-company').forEach(el => {
            el.classList.add('bubble-text');
        });

        // Add bubble effect to stat numbers
        document.querySelectorAll('.stat-number, .stat-label').forEach(el => {
            el.classList.add('bubble-text');
        });

        // Add bubble effect to icons and SVGs
        document.querySelectorAll('.logo-icon, svg, .company-icon').forEach(el => {
            el.classList.add('bubble-icon');
        });

        // Add bubble effect to buttons
        document.querySelectorAll('.category-btn, .tab-btn').forEach(el => {
            el.classList.add('bubble-btn');
        });

        // Add bubble effect to interactive elements
        document.querySelectorAll('.model-card, .stat-item, .opensource-item, .compare-card, .highlight-panel').forEach(el => {
            el.classList.add('bubble-element');
        });

        // Add float animation to key elements
        document.querySelectorAll('.logo-icon, .hero-badge, .nav-brand').forEach((el, index) => {
            el.classList.add('bubble-float');
            el.style.animationDelay = `${index * 0.5}s`;
        });

        // Add bubble effect to chart bars
        document.querySelectorAll('.chart-bar').forEach(el => {
            el.addEventListener('mouseenter', function() {
                this.style.filter = 'brightness(1.2)';
            });
            el.addEventListener('mouseleave', function() {
                this.style.filter = 'brightness(1)';
            });
        });

        // Add bubble effect to search bar
        const searchBar = document.querySelector('.search-bar');
        if (searchBar) {
            searchBar.addEventListener('mouseenter', function() {
                this.style.transform = 'translateY(-3px) scale(1.01)';
            });
            searchBar.addEventListener('mouseleave', function() {
                this.style.transform = '';
            });
        }

        // Add bubble effect to nav links
        document.querySelectorAll('.nav-link').forEach(el => {
            el.addEventListener('mouseenter', function() {
                this.style.transform = 'translateY(-2px)';
            });
            el.addEventListener('mouseleave', function() {
                this.style.transform = '';
            });
        });
    }
};
