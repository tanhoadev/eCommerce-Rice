(function($) {
    "use strict";

    // Dropdown on mouse hover
    $(document).ready(function() {
        function toggleNavbarMethod() {
            if ($(window).width() > 992) {
                $('.navbar .dropdown').on('mouseover', function() {
                    $('.dropdown-toggle', this).trigger('click');
                }).on('mouseout', function() {
                    $('.dropdown-toggle', this).trigger('click').blur();
                });
            } else {
                $('.navbar .dropdown').off('mouseover').off('mouseout');
            }
        }
        toggleNavbarMethod();
        $(window).resize(toggleNavbarMethod);
    });


    // Back to top button
    $(window).scroll(function() {
        if ($(this).scrollTop() > 100) {
            $('.back-to-top').fadeIn('slow');
        } else {
            $('.back-to-top').fadeOut('slow');
        }
    });
    $('.back-to-top').click(function() {
        $('html, body').animate({ scrollTop: 0 }, 1500, 'easeInOutExpo');
        return false;
    });


    // Vendor carousel
    $('.vendor-carousel').owlCarousel({
        loop: true,
        margin: 29,
        nav: false,
        autoplay: true,
        smartSpeed: 1000,
        responsive: {
            0: {
                items: 2
            },
            576: {
                items: 3
            },
            768: {
                items: 4
            },
            992: {
                items: 5
            },
            1200: {
                items: 6
            }
        }
    });


    // Related carousel
    $('.related-carousel').owlCarousel({
        loop: true,
        margin: 29,
        nav: false,
        autoplay: true,
        smartSpeed: 1000,
        responsive: {
            0: {
                items: 1
            },
            576: {
                items: 2
            },
            768: {
                items: 3
            },
            992: {
                items: 4
            }
        }
    });

    const minus = document.querySelectorAll(".btn-minus")
        // Product Quantity
    $('.quantity button').on('click', function() {
        var button = $(this);
        var oldValue = button.parent().parent().find('input#quantity').val();
        if (button.hasClass('btn-plus')) {
            var newVal = parseFloat(oldValue) + 1;
        } else {
            if (oldValue > 1) {
                var newVal = parseFloat(oldValue) - 1;
                minus[0].classList.remove("disable-btn")
            } else {
                newVal = 1;
                minus[0].classList.add("disable-btn")
            }
        }
        button.parent().parent().find('input#quantity').val(newVal);
    });

})(jQuery);

const starts = document.querySelectorAll(".starts i")
const valueCheckeds = document.querySelectorAll('.starts input[type="radio"]')

starts.forEach((star, index1) => {
    star.addEventListener("click", () => {
        starts.forEach((star, index2) => {
            if (index1 >= index2) {
                star.classList.add("active")
                star.classList.add("fas")
                star.classList.add("fa-star")
            } else {
                star.classList.remove("active")
                star.classList.remove("fas")
                star.classList.remove("fa-star")
                star.classList.add("far")
                star.classList.add("fa-star")
            }
            
        })
    })
})