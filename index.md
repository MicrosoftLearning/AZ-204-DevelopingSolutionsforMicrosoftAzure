---
title: Online Hosted Instructions
permalink: index.html
layout: home
---

# AZ-204: Developing solutions for Microsoft Azure

Hyperlinks to each of the lab exercises and demos are listed below.

## Labs

{% assign labs = site.pages | where_exp: "page", "page.url contains '/Instructions/Labs'" %}
| Module | Lab |
| --- | --- |
{% for activity in labs  %}{% if activity.lab.az204Module %}| {{ activity.lab.az204Module }} | [{{ activity.lab.title }}{% if activity.lab.type %} - {{ activity.lab.type }}{% endif %}]({{ site.github.url }}{{ activity.url }}) |
{% endif %}{% endfor %}
