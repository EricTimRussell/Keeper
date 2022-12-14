<template>

  <body>
    <div class="card text-light bgimg d-flex card-shadow" :style="{ backgroundImage: `url(${keeps.img})` }">
      <div class="card-body">
      </div>
      <div class="card-footer outline d-flex justify-content-between text-shadow">
        <h3 @click="getKeepById()" class="selectable" data-bs-toggle="modal" :data-bs-target="`#vaultKeep${keeps.id}`">
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
                <div class="d-flex justify-content-between p-1">
                  <div>
                    <i @click="removeKeepFromVault(keeps.vaultKeepId)" title="Delete"
                      class="mdi mdi-delete-forever selectable text-danger"></i>
                  </div>
                  <!-- Breaks on route to profile page -->
                  <!-- <router-link :to="{ name: 'Profile', params: { id: keeps.creatorId } }"> -->
                  <img :src=activeKeep.creator?.picture alt="profile pic" :title=profile.name class="profile-img mt-5">
                  <!-- </router-link> -->
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
import { Modal } from "bootstrap";
import { AppState } from "../AppState";
import { keepsService } from "../services/KeepsService";
import { vaultKeepsService } from "../services/VaultKeepsService";
import Pop from "../utils/Pop";


export default {
  props: {
    keeps: { type: Object, required: true }
  },
  setup(props) {
    return {
      account: computed(() => AppState.account),
      profile: computed(() => AppState.activeProfile),
      vault: computed(() => AppState.vaults),
      activeKeep: computed(() => AppState.activeKeep),

      // NOTE when removing vault keep, kept count only updates on refresh
      async removeKeepFromVault(vaultKeepId) {
        try {
          let confirm = await Pop.confirm()
          if (!confirm) {
            return
          }
          Modal.getOrCreateInstance(document.getElementById(`vaultKeep${props.keeps.id}`)).hide()
          await vaultKeepsService.removeKeepFromVault(vaultKeepId)
        } catch (error) {
          Pop.error(error, "Removing keep from vault")
        }
      },
      async getKeepById() {
        try {
          await keepsService.getKeepById(props.keeps.id)
        } catch (error) {
          Pop.error(error, "Get Keep By Id")
        }
      },
    }
  }
}
</script>


<style lang="scss" scoped>
body {
  letter-spacing: -1px;
  text-transform: lowercase;
}

.no-pad {
  padding: 0%;
}

.profile-img {
  border-radius: 50%;
  max-height: 5vh;
  min-width: 5vh;
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
</style>