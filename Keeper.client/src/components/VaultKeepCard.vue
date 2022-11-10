<template>

  <body>
    <div class="card text-light bgimg d-flex card-shadow" :style="{ backgroundImage: `url(${keeps.img})` }">
      <div class="card-body">
      </div>
      <div class="card-footer outline d-flex justify-content-between text-shadow">
        <h3 class="selectable" data-bs-toggle="modal" :data-bs-target="`#vaultKeep${keeps.id}`">
          {{ keeps.name }}
        </h3>
      </div>
    </div>
  </body>

  <!-- View Keep Modal in vault-->
  <div class="modal fade" :id="`vaultKeep${keeps.id}`" tabindex="-1" aria-labelledby="Vault Keep" aria-hidden="true">
    <div class="modal-dialog modal-xl modal-dialog-centered">
      <div class="modal-content">
        <div class="no-pad modal-body">
          <div class="container-fluid">
            <div class="row">
              <div class="col-md-6 no-pad">
                <img :src=keeps.img alt="Keep Image" class="img-fluid">
              </div>
              <div class="col-md-6 text-center d-flex flex-column justify-content-between px-5 py-1">
                <div class="d-flex justify-content-center">
                  <h5 class="mdi mdi-eye-outline">&nbsp{{ keeps.views }}&nbsp&nbsp</h5>
                  <h5 class="mdi mdi-alpha-k-box-outline">&nbsp{{ keeps.kept }}</h5>
                </div>
                <div>
                  <h1>{{ keeps.name }} </h1>
                  <p class="text-start">{{ keeps.description }} Lorem ipsum dolor sit amet consectetur adipisicing elit.
                    Accusamus animi
                    illo nulla, maxime repudiandae saepe molestiae alias excepturi odio, quo officiis repellendus,
                    corrupti rem est vero iste non blanditiis? Odit.</p>
                </div>
                <!-- Add Keep to Vault -->
                <div class="d-flex justify-content-between">
                  <div class="my-5 d-flex gap-3">
                    <div class="dropdown">
                      <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown"
                        aria-expanded="false">
                        Dropdown button
                      </button>
                      <ul class="dropdown-menu">
                        <li v-for="v in vault" :key="v.id">
                          <KeepModalDropdown :vault="v" />
                        </li>
                      </ul>
                    </div>
                  </div>
                  <img :src=profile.picture alt="profile pic" :title=profile.name class="profile-img mt-5">
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import Pop from "../utils/Pop";


export default {
  props: {
    keeps: { type: Keep, required: true }
  },
  setup(props) {
    return {
      profile: computed(() => AppState.account),
      vault: computed(() => AppState.vaults),
      async removeKeepFromVault() {
        try {
          // Finish this 
        } catch (error) {
          Pop.error(error, "Removing keep from vault")
        }
      },
    }
  }
}
</script>


<style lang="scss" scoped>
.no-pad {
  padding: 0%;
}

.bgimg {
  height: 35vh;
  background-position: center;
  background-size: cover;
}

.outline {
  border: none;
  background-color: #ffffff00
}

.card-shadow {
  box-shadow: 0 2px 10px -1px #00000033, 0 5px 10px 0 #00000070;
}

.text-shadow {
  text-shadow: 1px 1px 5px #ff00e1, 0 0 1em #8888f7, 0 0 0.2em #ffffff;
}
</style>